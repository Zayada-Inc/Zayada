using Microsoft.AspNetCore.Identity;

namespace ZayadaAPI.Errors
{
    public static class IdentityError
    {
        public static ApiValidationErrorResponse Response(IdentityResult result)
        {
            var resErr = result.Errors.Select(e => e.Description).ToList();
            var errorResponse = new ApiValidationErrorResponse
            {
                Errors = resErr
            };
            return errorResponse;
        }
    }
}
