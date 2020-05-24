using System;
using System.IO;
using System.Xml.Serialization;

namespace Powerfull.BLL.Services
{
	public class XmlFileSerializer : IDisposable
	{
		private string _tempXmlFile;


		public string SaveToTempFile<TReportType>(TReportType objectToReport)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(TReportType));

			//TODO: use store service to get folder for generating temp files
			this._tempXmlFile = $"{Guid.NewGuid().ToString()}.xml";

			using (FileStream serializeXmlStream =
				new FileStream(this._tempXmlFile, FileMode.Create))
			{
				serializer.Serialize(serializeXmlStream, objectToReport);
			}

			return this._tempXmlFile;
		}


		public void Dispose()
		{
			if (!string.IsNullOrEmpty(this._tempXmlFile)
				&& File.Exists(this._tempXmlFile))
			{
				File.Delete(this._tempXmlFile);
			}
		}
	}
}
