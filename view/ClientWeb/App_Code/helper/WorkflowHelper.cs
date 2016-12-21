using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ClientWeb
{
    public class WorkflowHelper
    {
        public static string GenerateExpression()
        {
            string strExpression = string.Empty;

            var lst = new List<DTOWFLEvent_Field>();

            string[] ArrayOpName = { "Equal", "NotEqual", "Great", "Less", "GreaterOrEqual", "LesserOrEqual",
                                     "EqualField", "NotEqualField", "GreatField", "LessField", "GreaterOrEqualField",
                                     "LesserOrEqualField" };
            string[] ArrayOpVal = { "==", "!=", ">", "<", ">=", "<=" };
            string[] ArrayOpCodeName = { "Or", "And" };
            string[] ArrayOpCodeVal = { "||", "&&" };
            int plat = 0;
            strExpression = "(";
            foreach (var x in lst)
            {
                string OperatorCode = "";
                if (plat != 0)
                {
                    for (int i = 0; i < ArrayOpCodeName.Length; i++)
                    {
                        if (ArrayOpCodeName[i] == x.OperatorCode)
                        {
                            OperatorCode = ArrayOpCodeVal[i];
                            break;
                        }
                    }
                }
                string OperatorValue = "";
                for (int i = 0; i < ArrayOpName.Length; i++)
                {
                    if (ArrayOpName[i] == x.OperatorValue)
                    {

                        int t = 0;
                        if (i > 5)
                        {
                            t = i - 6;
                        }
                        else
                            t = i;
                        OperatorValue = ArrayOpVal[t];
                        break;
                    }
                }
                strExpression = strExpression + OperatorCode + x.FieldCode + OperatorValue + x.CompareValue;
                plat = 1;
            }
            strExpression = strExpression + ");";
            return strExpression;
        }
    }
}