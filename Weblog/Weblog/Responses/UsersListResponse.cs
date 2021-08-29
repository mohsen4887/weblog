using System.Collections.Generic;
using Weblog.ViewModels;

namespace Weblog.Responses
{
    public class UsersListResponse
    {
        public List<UserVM> Data { get; set; }
        public int TotalUsers { get; set; }

        public UsersListResponse()
        {
            
        }
        public UsersListResponse(List<UserVM> data, int totalUsers)
        {
            Data = data;
            TotalUsers = totalUsers;
        }
    }
}
