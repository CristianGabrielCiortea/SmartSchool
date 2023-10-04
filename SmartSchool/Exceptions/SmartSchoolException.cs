using System;

namespace SmartSchool.Exceptions
{
    public class SmartSchoolException : ApplicationException
    {
        public SmartSchoolException(string message)
            : base(message) { }
    }
}