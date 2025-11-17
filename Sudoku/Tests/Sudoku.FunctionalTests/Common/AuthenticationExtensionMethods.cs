using Sudoku.Application.DTOs.Account.Responses;
using Sudoku.Application.Wrappers;

namespace Sudoku.FunctionalTests.Common
{
    public static class AuthenticationExtensionMethods
    {
        public static async Task<AuthenticationResponse> GetGhostAccount(this HttpClient client)
        {
            var url = ApiRoutes.Account.Start;

            var result = await client.PostAndDeserializeAsync<BaseResult<AuthenticationResponse>>(url);

            return result.Data;
        }
    }
}
