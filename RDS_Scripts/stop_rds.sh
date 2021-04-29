# DB was migrated from AWS to Azure and the script is no longer in use.
# Turn off AWS RDS

#!/bin/bash
PATH=/usr/local/bin:/usr/local/sbin:~/bin:/usr/bin:/bin:/usr/sbin:/sbin

# instance_names is provided as a script paramter,
# whicch is a comma separated list of RDS instances.
# As of today, I have only one instance: mycompany
# Usage example:  ./stop_rds.sh data-dev-mariadb

instance_names=$1
region="us-east-1"
for db_instance in $(echo $instance_names | sed "s/,/ /g"); do
    # db_instance_snapshot=${db_instance}"-snapshot-"`date "+%Y-%m-%dT%H-%M-%S"`
    # stop_command="/usr/local/bin/aws rds stop-db-instance --region "$region" --db-instance-identifier "$db_instance" --db-snapshot-identifier "$db_instance_snapshot

    stop_command="/usr/local/bin/aws rds stop-db-instance --region "$region" --db-instance-identifier "$db_instance
    echo "Stopping RDS instance: "$db_instance
    output=`$stop_command`
    echo "${output}"
done
