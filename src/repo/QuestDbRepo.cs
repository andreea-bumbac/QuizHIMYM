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
    public class QuestDbRepo : IQuestRepo
    {
        private static readonly ILog Log = LogManager.GetLogger("QuestDbRepo");
        private readonly IDictionary<string, string?> _props;

        public QuestDbRepo(IDictionary<string, string?> props)
        {
            Log.Info("Creating QuestDBRepo ");
            _props = props;
        }

        public Quest FindById(long id)
        {
            Log.InfoFormat("Entering FindById with value {0}", id);
            var connection = DbUtils.GetConnection(_props);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id,question,firstAnswer,secondAnswer,thirdAnswer,correctAnswer FROM Quest WHERE id=@id";
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Quest quest = Extract(dataReader);
                        Log.InfoFormat("Exiting FindById with value {0}", quest);
                        return quest;
                    }
                }
            }
            Log.InfoFormat("Exiting FindById with value {0}", null);
            return null;
        }

        public IEnumerable<Quest> FindAll()
        {
            Log.InfoFormat("Entering FindAll");
            var connection = DbUtils.GetConnection(_props);
            IList<Quest> qsts = new List<Quest>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id,question,firstAnswer,secondAnswer,thirdAnswer,correctAnswer FROM Quest ";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Quest quest = Extract(dataReader);
                        qsts.Add(quest);
                    }
                }
            }
            Log.InfoFormat("Exiting FindAll with value {0}", qsts);
            return qsts;
        }

        public void Save(Quest newEntity)
        {
            Log.InfoFormat("Entering Save with value {0}", newEntity);
            var connection = DbUtils.GetConnection(_props);

            using var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO Quest(question,firstAnswer,secondAnswer,thirdAnswer,correctAnswer) " +
                "VALUES(@question,@firstAnswer,@secondAnswer,@thirdAnswer,@correctAnswer);";
            var question = command.CreateParameter();
            question.ParameterName = "@question";
            question.Value = newEntity.question;
            command.Parameters.Add(question);

            var firstAnswer = command.CreateParameter();
            firstAnswer.ParameterName = "@firstAnswer";
            firstAnswer.Value = newEntity.firstAnswer;
            command.Parameters.Add(firstAnswer);

            var secondAnswer = command.CreateParameter();
            secondAnswer.ParameterName = "@secondAnswer";
            secondAnswer.Value = newEntity.firstAnswer;
            command.Parameters.Add(secondAnswer);

            var thirdAnswer = command.CreateParameter();
            thirdAnswer.ParameterName = "@thirdAnswer";
            thirdAnswer.Value = newEntity.thirdAnswer;
            command.Parameters.Add(thirdAnswer);

            var correctAnswer = command.CreateParameter();
            correctAnswer.ParameterName = "@correctAnswer";
            correctAnswer.Value = newEntity.firstAnswer;
            command.Parameters.Add(correctAnswer);

            var result = command.ExecuteNonQuery();
            Log.InfoFormat("Added {0} entities", result);
        }

        public void Delete(long id)
        {
            Log.InfoFormat("Entering Delete with value {0}", id);
            var connection = DbUtils.GetConnection(_props);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Quest WHERE id=@id";
            var paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);
            var result = command.ExecuteNonQuery();
            Log.InfoFormat("Deleted {0} entities", result);
        }





        private Quest Extract(IDataReader dataReader)
        {
            var id = dataReader.GetInt64(0);
            var question = dataReader.GetString(1);
            var firstAnswer = dataReader.GetString(2);
            var secondAnswer = dataReader.GetString(3);
            var thirdAnswer = dataReader.GetString(4);
            var correctAnswer = dataReader.GetString(5);


            var Qs = new Quest(question, firstAnswer, secondAnswer, thirdAnswer, correctAnswer)
            {
                Id = id
            };
            return Qs;
        }

        

        


    }
}
