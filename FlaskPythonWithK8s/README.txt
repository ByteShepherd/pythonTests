Build the Docker image using the following command:
docker build -t tuschinski/flask-on-k8s:latest .

Push the image to docker hub
docker push tuschinski/flask-on-k8s:latest

Apply the deployment, creating the PODs
kubectl apply -f deployment.yaml

=============

Minikube
Source: https://minikube.sigs.k8s.io/docs/start/?arch=%2Flinux%2Fx86-64%2Fstable%2Fdebian+package#Ingress

Install
curl -LO https://storage.googleapis.com/minikube/releases/latest/minikube_latest_amd64.deb
sudo dpkg -i minikube_latest_amd64.deb

Start your cluster
minikube start --force --driver=docker

Alias
alias kubectl="minikube kubectl --"

Dashboard
minikube dashboard

Open Tunnel
minikube tunnel

Discover the IP from tunneling
kubectl get services

==============



