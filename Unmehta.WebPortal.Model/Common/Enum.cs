using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Common
{

    public class EnumClass
    {

        public enum Month
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
        public enum ChartType
        {
            Bar,
            Pie
        }

        public enum TimeTT
        {
            AM,
            PM
        }

        public enum VisibityType
        {
            mycandidate,
            /// <summary>Show grigview panel</summary>
            GridView,
            /// <summary>Show view panel</summary>
            View,
            /// <summary>Show insert panel</summary>
            Insert,
            /// <summary>Show edit panel</summary>
            Edit,
            /// <summary>Show save & continue panel</summary>
            SaveAndAdd,
            /// <summary>Verify panel</summary>
            Verify,
            /// <summary>Print Record</summary>
            Print,

            Schedule
        }

        public enum PopupMessageType
        {
            success,
            error,
            warning,
            info
        }
        public enum GenderType
        {
            [StringValue("Male")]
            Male = 1,
            [StringValue("Female")]
            Female = 2,
            [StringValue("Others")]
            Others = 3
        }

        public enum EducationType
        {
            [StringValue("10th Board")]
            HSC = 1,
            [StringValue("12th Board")]
            CBSE = 2,
            [StringValue("Pre Graduate")]
            PreGraduate = 3,
            [StringValue("Graduate")]
            Graduate = 4,
            [StringValue("Post Graduate")]
            Postgraduate = 5,
            [StringValue("PhD")]
            PhD = 6,
            [StringValue("Others")]
            Others = 7
        }

        public enum MaritalStatusType
        {
            [StringValue("Single")]
            Single = 1,
            [StringValue("Married")]
            Married =2,
            [StringValue("Widowed")]
            Widowed =3,
            [StringValue("Divorsed")]
            Divorsed =4,
            [StringValue("Others")]
            Others = 5
        }
        public enum RecrutmentView
        {
            JobSelect,
            BasicDetail,
            EducationDetail,
            ExperianceDetail,
            FamilyDetail,
            CourseDetail,
            ProfestionalRefrellDetail,
            ExtraFieldDetail
        }

        public class StringValue : System.Attribute
        {
            private readonly string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }

        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static class StringEnum
        {
            public static string GetStringValue(Enum value)
            {
                string output = null;
                Type type = value.GetType();

                //Check first in our cached results...

                //Look for our 'StringValueAttribute' 

                //in the field's custom attributes

                FieldInfo fi = type.GetField(value.ToString());
                StringValue[] attrs =
                   fi.GetCustomAttributes(typeof(StringValue),
                                           false) as StringValue[];
                if (attrs.Length > 0)
                {
                    output = attrs[0].Value;
                }

                return output;
            }
        }
        public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (int value in Enum.GetValues(enumerationType))
            {
                var name = Enum.GetName(enumerationType, value);
                dictionary.Add(value, name);
            }

            return dictionary;
        }

        public enum RequirtmentTypes
        {
            walkin,
            wanted,
            Online
        }

    }
}
