using KantarStore.Domain.Entities;
using KantarStore.Infrastructure.Persistance;

namespace KantarStore.Infrastructure.Seeders
{
    internal class UserSeeder(KantarStoreDBContext dBContext) : IUserSeeder
    {
        public async Task Seed()
        {
            if (await dBContext.Database.CanConnectAsync())
            {
                if (!dBContext.Users.Any())
                {
                    var users = GetUsers();

                    dBContext.Users.AddRange(users);

                    await dBContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();

            var user1 = new User(
                firstName: "Pedro",
                email: "pedrolopesmelo@gmail.com",
                passwordHash: "1234.abcd",
                lastName: "Melo",
                phone: "+1234567890"
            );

            var user2 = new User(
                firstName: "Anna",
                email: "anna.smith@outlook.com",
                passwordHash: "secureHash456",
                lastName: "Smith",
                phone: "+447700900123"
            );

            var user3 = new User(
                firstName: "Carlos",
                email: "carlos.fernandez@kantar.com",
                passwordHash: "strongHash789",
                lastName: "Fernandez",
                phone: "+349600112233"
            );

            users.Add(user1);
            users.Add(user2);
            users.Add(user3);

            return users;
        }
    }
}
