using System.Collections.Generic;

namespace Greg.Xrm.RoleEditor.Model
{
	public class PrivilegeClassification
	{
		public static Dictionary<string, string[]> DefaultForMisc { get; } = new Dictionary<string, string[]>() {
			{ "General", new string[] {
				"prvExportToExcel",
				"prvBulkDelete",
				"prvBulkEdit",
				"prvDocumentGeneration",
				"prvDownloadFetchXml",
				"prvMerge",
				"prvMailMerge",
				"prvWebMailMerge",
				"prvPrint",
				"prvSendAppNotification",
				"prvReadLicense",
				"prvRetrieveMultipleSocialInsights",
				"prvRollupGoal",
				"prvAdminFilter",
				"prvApproveRejectEmailAddress",
				"prvEmbeddedPBIDatasetAnalyzeInExcel",
				"prvOverrideCreatedOnCreatedBy",
				"prvOverridePriceEngineInvoice",
				"prvOverridePriceEngineOpportunity",
				"prvOverridePriceEngineOrder",
				"prvOverridePriceEngineQuote",
				"prvAllowQuickCampaign",
				"prvControlDecrementTerm",
				"prvPortalApplication",
				"prvPostRuntimeIntegrationExternalEvent",
				"prvCreateOrgEmailTemplates",
				"prvPublishOrgMailMergeTemplate",
				"prvPublishDuplicateRule",
				"prvPublishOrgReport",
				"prvPublishRSReport",
				"prvQOIOverrideDelete"
			}},
			{ "Audit management", new string[] {
				"prvReadRecordAuditHistory",
				"prvReadAuditPartitions",
				"prvReadAuditSummary",
				"prvDeleteAuditPartitions",
				"prvDeleteRecordChangeHistory",
			}
			},
			{
				"Customizations", new string[] {
				"prvImportCustomization",
				"prvExportCustomization",
				"prvISVExtensions",
				"prvLanguageSettings",
				"prvPublishCustomization",
			}},
			{
				"Business logic", new string[] {
				"prvActivateBusinessProcessFlow",
				"prvActivateBusinessRule",
				"prvActivateSynchronousWorkflow",
				"prvBypassCustomBusinessLogic",
				"prvBypassCustomPlugins",
				"prvBypassMSBusinessLogic",
				"prvFlow",
				"prvWorkflowExecution",
			}},
			{
				"Calendar", new string[] {
				"prvReadOwnCalendar",
				"prvCreateOwnCalendar",
				"prvWriteOwnCalendar",
				"prvDeleteOwnCalendar",
				"prvSearchAvailability",
				"prvAddressBook",
				"prvWriteBusinessClosureCalendar",
				"prvWriteHolidayScheduleCalendar",
			}},
			{
				"Knowledge base", new string[] {
				"prvLearningPath",
				"prvPublishArticle",
				"prvApproveKnowledgeArticle",
				"prvPublishKnowledgeArticle",
			}},
			{
				"User management", new string[] {
				"prvAssignManager",
				"prvAssignPosition",
				"prvAssignTerritory",
				"prvBrowseAvailability",
				"prvDisableBusinessUnit",
				"prvDisableUser",
				"prvPromoteToAdmin",
				"prvRunPrivilegeCheckerForAnotherUser",
				"prvReparentBusinessUnit",
				"prvReparentTeam",
				"prvReparentUser",
				"prvWriteHierarchicalSecurityConfiguration",
			}},
			{
				"Impersonation", new string[] {
				"prvActOnBehalfOfAnotherUser",
				"prvActOnBehalfOfExternalParty",
				"prvSendAsUser",
			}},
			{
				"External app use", new string[] {
				"prvGoOffline",
				"prvUseTabletApp",
				"prvUseOfficeApps",
				"prvSendInviteForLive",
				"prvSyncToOutlook",
			}},
			{
				"Technical stuff", new string[] {
				"prvAllowTDSAccess",
				"prvAdminReadExchangeSyncIdMapping",
				"prvChangeSqlEncryptionKey",
				"prvRestoreSqlEncryptionKey",
				"prvReadSqlEncryptionKey",
				"prvTurnDevErrorsOnOff",
			}},
			{
				"Integrations", new string[] {
				"prvUseInternetMarketing",
				"prvConfigureInternetMarketing",
				"prvConfigureSharePoint",
				"prvConfigureYammer",
				"prvGetmsdynmkt_experimentationSetting",
				"prvIntelligenceAdministration",
				"prvIntelligenceUsage",
				"prvManageDataSyncs",
				"prvOneDrive",
				"prvRefreshRuntimeIntegrationComponents",
				"prvUnlinkTeamsChatFromRecord",
			}},
			{
				"Power BI", new string[] {
				"prvPowerBIWorkspaceAdmin",
				"prvPowerBIWorkspaceContributor",
				"prvPowerBIWorkspaceViewer",
			}}
		};
	}
}
