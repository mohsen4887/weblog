using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Requests
{
    public class UsersListRequest
    {
        public string Query { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; } = 1;

        public int PerPage { get; set; } = 15;

        public UsersListRequest()
        {
            
        }
        public UsersListRequest(string query, string sort, int page, int perPage)
        {
            Query = query;
            Sort = sort;
            Page = page;
            PerPage = perPage;
        }
    }
}
