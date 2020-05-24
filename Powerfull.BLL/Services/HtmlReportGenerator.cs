using System.IO;
using System.Xml.Xsl;

namespace Powerfull.BLL.Services
{
	public static class HtmlReportGenerator
	{
		public static string Generate<TReportType>(TReportType objectToReport)
		{
			string resultReport = $"{typeof(TReportType).Name}.html";

			using (XmlFileSerializer xmlFileSerializer = new XmlFileSerializer())
			{
				string xmlSerializedObject = xmlFileSerializer.SaveToTempFile(objectToReport);

				XslCompiledTransform xslt = new XslCompiledTransform();
				//TODO: use store service for getting folder for searching requirement xsl-styles
				string stylesheetUri = $"{Directory.GetCurrentDirectory()}/Xslt/Styles/{typeof(TReportType).Name}.xslt";

				xslt.Load(stylesheetUri);

				xslt.Transform(xmlSerializedObject, resultReport);
			}

			return resultReport;
		}
	}
}
