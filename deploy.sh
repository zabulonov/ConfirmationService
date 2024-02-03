environment="$1"
echo "environment is set to: $environment"
docker build . --build-arg CUSTOM_ENV_VAR=$environment -t confirmation_service --no-cache
docker-compose up -d 