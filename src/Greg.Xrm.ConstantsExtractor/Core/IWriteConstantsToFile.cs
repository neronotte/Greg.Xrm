namespace Greg.Xrm.ConstantsExtractor.Core
{
	public interface IWriteConstantsToFile
	{
		void WriteConstantsToFile();

		void WriteEntityConstantsClass();

		void WriteGlobalOptionSetConstants();
	}
}
