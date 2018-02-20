using StoredProcedurePlus.Net.EntityManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class MockEventArgs:EventArgs
    {
        public MockEventArgs(IDataEntityAdapter input):base()
        {
            Input = input;
        }

        public IDataEntityAdapter Input { get; private set; }
        public int Result { get; set; }
    }

    public delegate void MockExecutionHandler(object sender, MockEventArgs e);

    public abstract class StoredProcedureManager<S> where S : class, new()
    {
        #region Restricted 

        static object Locker = new object();

        readonly static ProcedureConfiguration<S> Configuration = new ProcedureConfiguration<S>();

        private void Initialize()
        {
            lock (Locker)
            {
                if (Configuration.ProcedureName == null)
                {
                    Setup(Configuration);

                    Configuration.Initialize();

                    if (Configuration.ConnectionString == null)
                    {
                        Configuration.Connection.SetConnectionStringName(Configuration.ConnectionStringName);
                    }
                    else
                    {
                        Configuration.Connection.SetConnectionString(Configuration.ConnectionString);
                    }
                }
            }
        }

        protected StoredProcedureManager()
        {

        }

        protected abstract void Setup(ProcedureConfiguration<S> configuration);

        List<List<object>> ResultSet = new List<List<object>>();

        #endregion

        #region Public Events 
        public event MockExecutionHandler OnMockExecution = null;
        #endregion

        #region Public Methods

        [SuppressMessage("Microsoft.Security", "CA2100", Justification = "The command text is not user given")]
        public int Execute(S input, ConnectionScope scope)
        {
            this.Initialize();

            IDbCommand Command = new SqlCommand(Configuration.ProcedureName)
            {
                CommandType = CommandType.StoredProcedure
            };

            CommandBehavior Behavior = CommandBehavior.Default;

            int Result = -1;

            if (scope == null)
            {
                using (scope = new ConnectionScope())
                {
                    scope.SetConnectionProvider(Configuration.Connection);

                    DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.Input.GetAsDbParameters();

                    for (int i = 0; i < adapter.FieldCount; i++)
                    {
                        Command.Parameters.Add(adapter[i]);
                    }

                    Configuration.Input.Prepare(adapter);

                    if (adapter.FieldCount > 0)
                    {
                        Configuration.Input.Get(input, adapter);
                    }

                    if (Configuration.OutputSets.Any())
                    {
                        int ResultSetIndex = 0;

                        if (Configuration.Mock)
                        {
                            if (OnMockExecution != null)
                            {
                                MockEventArgs Args = new MockEventArgs(adapter);
                                OnMockExecution?.Invoke(this, Args);
                                Result = Args.Result;
                            }
                        }
                        else
                        {
                            Command.Connection = scope.Create();

                            if (Command.Connection != null)
                            {
                                using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                                {
                                    do
                                    {
                                        ResultSet.Add(new List<object>());

                                        if (DataReader.Read())
                                        {
                                            NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                            DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                            c.Prepare(EntityAdapter);

                                            object Entity = Configuration.OutputSets[ResultSetIndex].CreateNewDefaultInstance();

                                            c.Set(EntityAdapter, Entity);

                                            ResultSet[ResultSetIndex].Add(Entity);

                                            while (DataReader.Read())
                                            {
                                                Entity = Configuration.OutputSets[ResultSetIndex].CreateNewDefaultInstance();

                                                c.Set(EntityAdapter, Entity);

                                                ResultSet[ResultSetIndex].Add(Entity);
                                            }
                                        }

                                        ResultSetIndex++;

                                    } while (DataReader.NextResult());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Configuration.Mock)
                        {
                            if (OnMockExecution != null)
                            {
                                MockEventArgs Args = new MockEventArgs(adapter);
                                OnMockExecution?.Invoke(this, Args);
                                Result = Args.Result;
                            }
                        }
                        else
                        {
                            Command.Connection = scope.Create();

                            if (Command.Connection != null)
                            {
                                Result = Command.ExecuteNonQuery();
                            }
                        }
                    }

                    Configuration.Input.Set(adapter, input);
                }
            }
            else
            {
                try
                {
                    scope.SetConnectionProvider(Configuration.Connection);

                    DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.Input.GetAsDbParameters();

                    for (int i = 0; i < adapter.FieldCount; i++)
                    {
                        Command.Parameters.Add(adapter[i]);
                    }                        

                    Configuration.Input.Prepare(adapter);

                    if (adapter.FieldCount > 0)
                    {
                        Configuration.Input.Get(input, adapter);
                    }

                    if (Configuration.OutputSets.Any())
                    {
                        int ResultSetIndex = 0;

                        if (Configuration.Mock)
                        {
                            if (OnMockExecution != null)
                            {
                                MockEventArgs Args = new MockEventArgs(adapter);
                                OnMockExecution?.Invoke(this, Args);
                                Result = Args.Result;
                            }
                        }
                        else
                        {
                            Command.Connection = scope.Create();

                            if (Command.Connection != null)
                            {
                                using (IDataReader DataReader = Command.ExecuteReader(Behavior))
                                {
                                    do
                                    {
                                        ResultSet.Add(new List<object>());

                                        if (DataReader.Read())
                                        {
                                            NonPrimitiveEntityConfiguration c = Configuration.OutputSets[ResultSetIndex];

                                            DbDataEntityAdapter EntityAdapter = c.GetNewDataAdapter(DataReader);

                                            c.Prepare(EntityAdapter);

                                            object Entity = Configuration.OutputSets[ResultSetIndex].CreateNewDefaultInstance();

                                            c.Set(EntityAdapter, Entity);

                                            ResultSet[ResultSetIndex].Add(Entity);

                                            while (DataReader.Read())
                                            {
                                                Entity = Configuration.OutputSets[ResultSetIndex].CreateNewDefaultInstance();

                                                c.Set(EntityAdapter, Entity);

                                                ResultSet[ResultSetIndex].Add(Entity);
                                            }
                                        }

                                        ResultSetIndex++;

                                    } while (DataReader.NextResult());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Configuration.Mock)
                        {
                            if (OnMockExecution != null)
                            {
                                MockEventArgs Args = new MockEventArgs(adapter);
                                OnMockExecution?.Invoke(this, Args);
                                Result = Args.Result;
                            }
                        }
                        else
                        {
                            Command.Connection = scope.Create();

                            if (Command.Connection != null)
                            {
                                Result = Command.ExecuteNonQuery();
                            }
                        }
                    }

                    Configuration.Input.Set(adapter, input);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    scope.End();
                }
            }
            return Result;
        }

        public int Execute(S input)
        {
            return this.Execute(input, null);
        }

        public IEnumerable<T> GetResult<T>(int index) where T : class
        {
            return ResultSet[index].Cast<T>();
        }

        public IEnumerable<T> GetResult<T>() where T : class
        {
            return this.GetResult<T>(0);
        }

        #endregion
    }
}
