namespace DAL.Tools
{
    public static class AppointmentDateScope
    {
        public static string SetScope(string query, int scope, int isWhere)
        {
            DateTime Today = DateTime.Today.Date;
            if (isWhere == 1)
            {
                if (scope == 1)
                    return query + " AND AppointmentDate >= \'" + Today + "\';";
                else
                    return query + " AND AppointmentDate < \'" + Today + "\';";
            }
            else
            {
                if (scope == 1)
                    return query + " WHERE AppointmentDate >= \'" + Today + "\';";
                else
                    return query + " WHERE AppointmentDate < \'" + Today + "\';";
            }

        }
    }
}
