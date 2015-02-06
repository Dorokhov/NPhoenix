using System.Linq;
using java.lang;
using java.lang.reflect;
using net.sf.jni4net.jni;
using Object = java.lang.Object;

namespace NPhoenix.Interop
{
    internal static class DriverReflection
    {
        internal static Method GetConnection()
        {
            Method getConnectionMethod = Class
                .forName(Constants.Classes.DriverManager, true, ClassLoader.getSystemClassLoader())
                .getDeclaredMethods()
                .Single(x => x.getName() == Constants.Methods.GetConnection
                             && x.GetSignature() == Constants.MethodSignatures.GetConnection_String_Connection);
            return getConnectionMethod;
        }

        internal static Object CreateStatement(this Object target)
        {
            return target.Invoke<Object>(Constants.Methods.CreateStatement, Constants.MethodSignatures.CreateStatement_Statement, new object[0]);
        }

        internal static Method GetExecuteUpdate(this Object target)
        {
            return target
                .getClass()
                .getMethods()
                .Single(x => x.getName() == Constants.Methods.ExecuteUpdate
                             && x.GetSignature() == Constants.MethodSignatures.ExecuteUpdate_String_Int);
        }

        internal static Object PrepareStatement(this Object target, string query)
        {
            return target.Invoke<Object>(
                Constants.Methods.PrepareStatement,
                Constants.MethodSignatures.PreparedStatement_String_PreparedStatement,
                new object[]
                {
                    query
                });
        }

        internal static Object ExecuteQuery(this Object target)
        {
            return target.Invoke<Object>(Constants.Methods.ExecuteQuery, Constants.MethodSignatures.ExecuteQuery_ResultSet, new object[0]);
        }

        internal static bool Next(this Object target)
        {
            return target.Invoke<bool>(Constants.Methods.Next, Constants.MethodSignatures.Bool, new object[0]);
        }

        internal static string GetString(this Object target, string columnName)
        {
            var getString = target
                .getClass()
                .getMethods()
                .Single(x => x.getName() == Constants.Methods.GetString &&
                             x.GetSignature() == Constants.MethodSignatures.String_String);
            java.lang.String result = (java.lang.String) getString.invoke(target, new[] {JNIEnv.ThreadEnv.NewString(columnName)});
            return result;
        }

        internal static void Close(this Object target)
        {
            target.Invoke(Constants.Methods.Close, Constants.MethodSignatures.Void);
        }

        internal static void Commit(this Object target)
        {
            target.Invoke(Constants.Methods.Commit, Constants.MethodSignatures.Void);
        }
    }
}
