using Microsoft.EntityFrameworkCore;

namespace Server.Database
{
    public sealed class ServerDb : DbContext
    {
        public ServerDb(DbContextOptions<ServerDb> options) : base(options)
        {
        }
    }
}
