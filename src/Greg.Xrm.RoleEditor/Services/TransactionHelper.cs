using Greg.Xrm.Logging;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace Greg.Xrm.RoleEditor.Services
{
	public static class TransactionHelper
	{
		// 0x80040203, returned by Dataverse when it cannot find a sub-request message in the sdkmessage table
		private const int UnknownMessageErrorCode = -2147220989;

		private static readonly Regex UnknownMessageRegex = new Regex(@"Message name '[^']+' does not exist", RegexOptions.Compiled);



		/// <summary>
		/// Executes the given requests in a single <see cref="ExecuteTransactionRequest"/>.
		/// If Dataverse rejects the transaction because one of the sub-request messages
		/// (e.g. AddPrivilegesRole) has no sdkmessage record, the requests are executed one by one.
		/// In that case atomicity is lost: if one of the requests fails, a <see cref="PartialSaveException"/>
		/// is thrown, describing which requests have been applied and which haven't.
		/// </summary>
		/// <param name="crm">The organization service.</param>
		/// <param name="requests">The requests to execute, in order.</param>
		/// <param name="log">The log where to write fallback details.</param>
		public static void ExecuteTransactionWithFallback(this IOrganizationService crm, IReadOnlyList<OrganizationRequest> requests, ILog log)
		{
			if (requests.Count == 0) return;

			var request = new ExecuteTransactionRequest();
			request.ReturnResponses = false;
			request.Requests = new OrganizationRequestCollection();
			request.Requests.AddRange(requests);

			try
			{
				crm.Execute(request);
				return;
			}
			catch (Exception ex) when (IsUnknownMessageFault(ex))
			{
				// the server validates the transaction before executing it, so nothing has been applied yet
				log.Warn($"ExecuteTransaction rejected by the server ({ex.Message}). Falling back to sequential execution of {requests.Count} request(s): atomicity is lost.");
			}

			var applied = new List<string>();
			for (var i = 0; i < requests.Count; i++)
			{
				try
				{
					crm.Execute(requests[i]);
					applied.Add(Describe(requests[i]));
				}
				catch (Exception ex)
				{
					var failed = Describe(requests[i]);
					var notExecuted = requests.Skip(i + 1).Select(Describe).ToList();

					var partialSaveException = new PartialSaveException(applied, failed, notExecuted, ex);
					log.Error(partialSaveException.GetDetails(), ex);
					throw partialSaveException;
				}
			}

			log.Info($"Fallback mode: {requests.Count} request(s) executed one by one.");
		}



		private static bool IsUnknownMessageFault(Exception ex)
		{
			for (var current = ex; current != null; current = current.InnerException)
			{
				if (current is FaultException<OrganizationServiceFault> fault && fault.Detail != null)
				{
					if (fault.Detail.ErrorCode == UnknownMessageErrorCode && UnknownMessageRegex.IsMatch(fault.Detail.Message ?? fault.Message))
						return true;

					continue;
				}

				// the connection library may wrap the original fault
				if (UnknownMessageRegex.IsMatch(current.Message ?? string.Empty))
					return true;
			}
			return false;
		}



		private static string Describe(OrganizationRequest request)
		{
			if (request is AddPrivilegesRoleRequest add)
				return $"{request.RequestName} ({add.Privileges?.Length ?? 0} privilege(s))";

			if (request is RemovePrivilegeRoleRequest remove)
				return $"{request.RequestName} ({remove.PrivilegeId})";

			if (request is CreateRequest create)
				return $"{request.RequestName} ({create.Target?.LogicalName})";

			if (request is UpdateRequest update)
				return $"{request.RequestName} ({update.Target?.LogicalName})";

			return request.RequestName;
		}
	}
}
