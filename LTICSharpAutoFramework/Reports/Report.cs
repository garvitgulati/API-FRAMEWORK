using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using LTICSharpAutoFramework.Utils;

namespace LTICSharpAutoFramework.Reports
{
	public class Report
	{
		static ILog log = LogUtils.GetLogger(typeof(Report));

		public static void PrintOperation(String operation)
		{
			log.Debug("\t\t\tOperation\t\t\t:\t\t\t" + operation);

		}

		public static void PrintKey(String key)
		{
			log.Debug("\t\t\tKey\t\t\t:\t\t\t" + key);
		}
		public static void PrintValue(String value)
		{
			log.Debug("\t\t\tValue\t\t\t:\t\t\t" + value);
		}
		public static void PrintValueType(String valueType)
		{
			log.Debug("\t\t\tValue Type\t\t\t:\t\t\t" + valueType);
		}
		public static void PrintData(String data)
		{
			log.Debug("\t\t\tData\t\t\t:\t\t\t" + data);
		}
		public static void PrintStatus(String status)
		{
			log.Debug("\t\t\tStatus\t\t\t:\t\t\t" + status + "\n");
		}
	}
}