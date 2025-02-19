using System.Globalization;

namespace Play.Common.Exceptions;

public class SettingException : Exception
{
    public SettingException(string message) : base(message)
    {
    }

    public SettingException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}