using Microsoft.IdentityModel.Tokens;

namespace BLL.Tools
{
    public class ToolSet
    {
        //public static string Message { get; set; } = default!;
        private static Action<string> LogMessage { get; set; } = default!;
        public ToolSet()
        {

        }
        public ToolSet(Action<string> logMessage)
        {
            LogMessage = logMessage;
        }

        /// <summary>
        /// Checks if object exists
        /// </summary>
        /// <param name="objectExists">bool if object exists</param>
        /// <param name="objectName">object being tested</param>
        /// <param name="txt">If you want a specific message to be returned in both case (true/false)</param>
        /// <returns>bool</returns>
        public bool ObjectExistsCheck(bool objectExists, string objectName, string txt = "")
        {
            return CheckTemplate(objectExists, objectName, "is not valid or doesn't exist!", "exists!", txt);
        }

        /// <summary>
        /// Checks if action was successful
        /// </summary>
        /// <param name="creationSuccess">Bool from the result of the method to be tested</param>
        /// <param name="objectName">In string, what object are we testing</param>
        /// <param name="objectAction">What action has been test : "created", "updated",...</param>
        /// <param name="txt">If you want a specific message to be returned in both case (true/false)</param>
        /// <returns>bool</returns>
        public bool SuccessCheck(bool isSuccess, string objectName, string objectAction, string txt = "")
        {

            return CheckTemplate(isSuccess, objectName, $"has/have not been {objectAction}!", $"has/have been {objectAction}!", txt);
        }

        private static bool CheckTemplate(bool check, string objectName, string ifFalse, string ifTrue, string txt = "")
        {
            if (check)
            {
                if (txt.IsNullOrEmpty())
                    LogMessage?.Invoke(objectName + " " + ifTrue);
                else
                    LogMessage?.Invoke(txt);
                return true;
            }

            if (txt.IsNullOrEmpty())
                LogMessage?.Invoke(objectName + " " + ifFalse);
            else
                LogMessage?.Invoke(txt);
            return false;
        }
    }
}
