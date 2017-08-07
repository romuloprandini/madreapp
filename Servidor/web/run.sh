#!/bin/bash
set -e

# Enter directory
cd  /var/www/html

# Change folder permission
chmod -R 0777 storage/
#chmod -R 0777 public/uploads/
chmod -R 0777 public/assets/

chown -R www-data:www-data /var/www/html

composer install

#install ssl (letsencrypt)
#Start Apache2 service

: "${APACHE_CONFDIR:=/etc/apache2}"
: "${APACHE_ENVVARS:=$APACHE_CONFDIR/envvars}"
if test -f "$APACHE_ENVVARS"; then
    . "$APACHE_ENVVARS"
fi

# Apache gets grumpy about PID files pre-existing
: "${APACHE_PID_FILE:=${APACHE_RUN_DIR:=/var/run/apache2}/apache2.pid}"
rm -f "$APACHE_PID_FILE"

exec apache2 -DFOREGROUND "$@"
