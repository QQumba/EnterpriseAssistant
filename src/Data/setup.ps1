$dbName='liquibase_test'

# connection using environment variable
$env:PGPASSWORD='rootservgeevich'
$dbExists=$(psql -U postgres -h localhost -p 5432 -AXqtc "SELECT 1 FROM pg_database WHERE datname = '$($dbName)';")
if(!$dbExists){
  psql -U postgres -h localhost -p 5432 -c "CREATE DATABASE $($dbName)"
}
else{
  echo "Database $($dbName) already exists"
}
$env:PGPASSWORD=''

# run liquibase update
liquibase update