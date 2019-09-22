using SCA.Common;
using System.Linq;
using System.Text;

namespace SCA.DapperRepository.Generic
{
    public static class QueryBuilder
    {
        /// <summary>
        /// Generate insert query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GenerateInsertQuery<T>(this T model, string tableName) where T : class
        {
            var propsArray = model.GetPropertiesOfModelAsString<T>().Split(',');
            var props = string.Join(',', propsArray.Where(x=>x != "@Id"));
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ({props.Replace('@', ' ')}) VALUES ({props}); " +
                $"SELECT LAST_INSERT_ID();");
            return insertQuery.ToString();
        }

        /// <summary>
        /// Generate update query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GenerateUpdateQuery<T>(this T model, string tableName) where T : class
        {
            var propsArray = model.GetPropertiesOfModelAsString<T>().Split(',');
            string props = "";
            foreach (var prop in propsArray.ToList().Where(x => x != "@Id"))
            {
                props += prop.Replace('@', ' ') + " = " + prop + ",";
            }
            props = props.TrimEnd(',');
            var updateQuery = new StringBuilder($"UPDATE {tableName} SET {props} WHERE Id = @Id;");
            return updateQuery.ToString();
        }

        /// <summary>
        ///Generate select query 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="tableName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GenerateSelectQuery<T>(this T model, string tableName, params string[] parameters) where T : class
        {
            var props = model.GetPropertiesOfModelAsString<T>();
            var whereParameters = parameters.SetWhereProperties();
            var selectQuery = new StringBuilder($"SELECT ({props.Replace('@', ' ')}) FROM {tableName} {whereParameters}");
            return selectQuery.ToString();
        }

        /// <summary>
        /// Generate delete query
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GenerateDeleteQuery(this string tableName)
        {
            var deleteQuery = new StringBuilder($"DELETE FROM {tableName} WHERE Id = @Id");
            return deleteQuery.ToString();
        }

        /// <summary>
        /// Generate delete range query
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GenerateDeleteRangeQuery(this string tableName)
        {
            var deleteQuery = new StringBuilder($"DELETE FROM {tableName} WHERE Id in @ids");
            return deleteQuery.ToString();
        }

    }
}
