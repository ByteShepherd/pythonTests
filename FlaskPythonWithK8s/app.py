from flask import request, Flask
import json
import os

app1 = Flask(__name__)
@app1.route('/')
def hello_world():
    pod_name = os.environ['POD_NAME']
    pod_ip = os.environ['POD_IP']
    return 'Olá POD:' + pod_name + ' IP:' + pod_ip

if __name__ == '__main__':
    app1.run(debug=True, host='0.0.0.0')