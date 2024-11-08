Build the Docker image using the following command:
docker build -t tuschinski/flask-on-k8s:latest .

Push the image to docker hub
docker push tuschinski/flask-on-k8s:latest

Apply the deployment, creating the PODs
kubectl apply -f deployment.yaml