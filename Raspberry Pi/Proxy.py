import socket
import requests
from datetime import datetime

UDP_IP = "192.168.0.64"  
UDP_PORT = 5005 
BUFFER_SIZE = 1024

# REST API URL 
REST_API_URL = "http://localhost:56238/api/biler"  

# UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind((UDP_IP, UDP_PORT))

print(f"Lytter på UDP broadcast på port {UDP_PORT}...")

while True:
    data, addr = sock.recvfrom(BUFFER_SIZE)  
    nummerplade = data.decode("utf-8")
    print(f"Modtaget besked fra {addr}: {nummerplade}")
    
    # payload til REST-kald
    payload = {"nummerplade": nummerplade}
    
    # POST request til REST API
    try:
        response = requests.post(url=REST_API_URL, json=payload)
        if response.status_code == 201:
            print("Data succesfuldt sendt til REST-service")
        elif response.status_code == 409:
            print("Data findes allerede i REST-service")
            response = requests.put(url=REST_API_URL, json={"nummerplade": nummerplade, "udkoerselstid": datetime.now()})
        else:
            print(f"Fejl ved REST-kald: {response.status_code} - {response.text}")
    except requests.exceptions.RequestException as e:
        print(f"Fejl ved forbindelse til REST-service: {e}")