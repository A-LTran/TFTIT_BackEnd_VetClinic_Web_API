using Microsoft.IdentityModel.Tokens;

namespace BLL.Tools
{
    public static class ToolSet
    {
        public static string Message { get; set; } = default!;

        public static bool ObjectExistsCheck(bool objectExists, string objectName, string txt = "")
        {
            return CheckTemplate(objectExists, objectName, "is not valid or doesn't exist!", "already exists!", txt);
        }

        public static bool SucceessCheck(bool creationSuccess, string objectName, string objectAction, string txt = "")
        {

            return CheckTemplate(creationSuccess, objectName, $"has not been {objectAction}! Something went wrong!", $"has been {objectAction}!", txt);
        }

        private static bool CheckTemplate(bool check, string objectName, string ifFalse, string ifTrue, string txt = "")
        {
            if (check)
            {
                if (txt.IsNullOrEmpty())
                    Message = objectName + " " + ifTrue;
                else
                    Message = txt;
                return true;
            }

            if (txt.IsNullOrEmpty())
                Message = objectName + " " + ifFalse;
            else
                Message = txt;
            return false;
        }
    }
}
