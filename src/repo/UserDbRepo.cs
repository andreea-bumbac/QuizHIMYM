using accesa.src.domain;
using log4net;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.repo
{
    public class UserDbRepo : IUserRepo
    {
        private static readonly ILog Log = LogManager.GetLogger("UserDbRepo");
        private readonly IDictionary<string, string?> _props;

        public UserDbRepo(IDictionary<string, string?> props)
        {
            Log.Info("Creating UserDBRepo ");
            _props = props;
        }

        public User FindById(long id)
        {
            Log.InfoFormat("Entering FindById with value {0}", id);
            var connection = DbUtils.GetConnection(_props);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id,username,parola,tokens, intr FROM User WHERE id=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        User user = Extract(dataReader);
                        Log.InfoFormat("Exiting FindById with value {0}", user);
                        return user;
                    }
                }
            }
            Log.InfoFormat("Exiting FindById with value {0}", null);
            return null;
        }

        public IEnumerable<User> FindAll()
        {
            Log.InfoFormat("Entering FindAll");
            var connection = DbUtils.GetConnection(_props);
            IList<User> users = new List<User>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id,username,parola,tokens, intr FROM User";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        User user = Extract(dataReader);
                        users.Add(user);
                    }
                }
            }
            Log.InfoFormat("Exiting FindAll with value {0}", users);
            return users;
        }

        public void Save(User newEntity)
        {
            Log.InfoFormat("Entering Save with value {0}", newEntity);
            var connection = DbUtils.GetConnection(_props);

            using var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO User(username, parola, tokens, intr) " +
                "VALUES(@username, @parola, @tokens, @intr);";
            var username = command.CreateParameter();
            username.ParameterName = "@username";
            username.Value = newEntity.username;
            command.Parameters.Add(username);

            var password = command.CreateParameter();
            password.ParameterName = "@parola";
            password.Value = newEntity.parola;
            command.Parameters.Add(password);

            var tokens = command.CreateParameter();
            tokens.ParameterName = "@tokens";
            tokens.Value = newEntity.tokens;
            command.Parameters.Add(tokens);

            var intr = command.CreateParameter();
            intr.ParameterName = "@intr";
            intr.Value = newEntity.intr;
            command.Parameters.Add(intr);

            var result = command.ExecuteNonQuery();
            Log.InfoFormat("Added {0} entities", result);
        }

        public void Delete(long id)
        {
            Log.InfoFormat("Entering Delete with value {0}", id);
            var connection = DbUtils.GetConnection(_props);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM User WHERE id=@id";
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);
            var result = command.ExecuteNonQuery();
            Log.InfoFormat("Deleted {0} entities", result);
        }

        

        public User GetByUsername(string username)
        {
            Log.InfoFormat("Entering GetByUsername with value {0}", username);
            var connection = DbUtils.GetConnection(_props);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id,username,parola, tokens, intr FROM User WHERE username=@username";
                var paramUsername = command.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                command.Parameters.Add(paramUsername);

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        User user = Extract(dataReader);
                        Log.InfoFormat("Exiting GetByUsername with value {0}", user);
                        return user;
                    }
                }
            }
            Log.InfoFormat("Exiting GetByUsername with value {0}", null);
            return null;
        }

        private User Extract(IDataReader dataReader)
        {
            var id = dataReader.GetInt64(0);
            var username = dataReader.GetString(1);
            var password = dataReader.GetString(2);
            var tokens = dataReader.GetInt32(3);
            var intr = dataReader.GetInt32(4);


            var user = new User(username, password, tokens, intr)
            {
                Id = id
            };
            return user;
        }

        public void Update(string username, string password, int newTokens, int newintr)
        {
            IEnumerable<User> users = FindAll();
            User user1 = new User("", "", 0, 0);
            foreach (var user in users)
            {
                if (user.username == username) { 
                    user1 = user;
                }
            }


            if (user1 != null)
            {
                Log.InfoFormat("Entering Update with value {0}", user1);
                var connection = DbUtils.GetConnection(_props);
                long id = user1.Id;

                using var command = connection.CreateCommand();
                command.CommandText =
                    "UPDATE User SET tokens=@tokens, intr=@intr WHERE id=@id";
                var tokens = command.CreateParameter();
                tokens.ParameterName = "@tokens";
                tokens.Value = user1.tokens;
                command.Parameters.Add(tokens);

                var intr = command.CreateParameter();
                intr.ParameterName = "@intr";
                intr.Value = user1.intr;
                command.Parameters.Add(intr);

               

                var result = command.ExecuteNonQuery();
                Log.InfoFormat("Updated {0} entities", result);
            }
        }


    }
}
