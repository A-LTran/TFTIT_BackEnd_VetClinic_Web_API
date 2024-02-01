namespace DAL.Tools
{
    public static class AppointmentDateScope
    {
        public static string SetScope(string query, int scope)
        {
            DateTime Today = DateTime.Today.Date;
            if (scope == 1)
                return query + " AND AppointmentDate >= \'" + Today + "\';";
            else
                return query + " AND AppointmentDate < \'" + Today + "\';";
        }
    }
}
