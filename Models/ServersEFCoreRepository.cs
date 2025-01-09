using Microsoft.EntityFrameworkCore;
using ServerManagement.Data;

namespace ServerManagement.Models
{
    public class ServersEFCoreRepository : IServersEFCoreRepository
    {
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<ServerManagementContext> dbContextFactory;

        public ServersEFCoreRepository(IDbContextFactory<ServerManagementContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public void AddServer(Server server)
        {
            using var db = this.dbContextFactory.CreateDbContext();
            db.Servers.Add(server);
            db.SaveChanges();
        }

        public List<Server> GetServers()
        {
            using var db = this.dbContextFactory.CreateDbContext();
            return db.Servers.ToList();
        }

        public List<Server> GetServerByCity(string cityName)
        {
            using var db = this.dbContextFactory.CreateDbContext();
            return db.Servers.Where(a => a.City != null && a.City.ToLower().IndexOf(cityName.ToLower()) >= 0).ToList();
        }

        public Server GetServerById(int id)
        {
            using var db = this.dbContextFactory.CreateDbContext();
            var server = db.Servers.Find(id);

            if (server is not null)
                return server;

            return new Server();
        }

        public void UpdateServer(int serverId, Server server)
        {
            if (server is null) throw new ArgumentNullException(nameof(server));
            if (serverId != server.ServerId) return;

            using var db = this.dbContextFactory.CreateDbContext();
            var serverToUpdate = db.Servers.Find(serverId);

            if (serverToUpdate is not null)
            {
                serverToUpdate.City = server.City;
                serverToUpdate.Name = server.Name;
                serverToUpdate.IsOnline = server.IsOnline;

                db.SaveChanges();
            }
        }

        public void DeleteServer(int serverId)
        {
            using var db = this.dbContextFactory.CreateDbContext();

            var serverToDelete = db.Servers.Find(serverId);
            if (serverToDelete is null) return;

            db.Servers.Remove(serverToDelete);
            db.SaveChanges();
        }

        public List<Server> SearchServers(string serverFilter)
        {
            using var db = this.dbContextFactory.CreateDbContext();
            return db.Servers.Where(a =>
                a.Name != null &&
                a.Name.ToLower().IndexOf(serverFilter.ToLower()) >= 0)
                .ToList();
        }
    }
}
