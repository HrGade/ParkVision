import socket
import time

#RaspberryPI simulering
PROXY_IP = "192.168.103.139"  # IP’en for proxy-servern
PROXY_PORT = 5005


PLATE = "AK57739"

def send_plate(plate):
    try:
        # opret TCP socket
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect((PROXY_IP, PROXY_PORT))
            print(f"Forbundet til proxy på {PROXY_IP}:{PROXY_PORT}")
            
            # Afsend Nummerplade
            s.sendall(plate.encode('utf-8'))
            print(f"Sendte nummerplade: {plate}")
    except Exception as e:
        print(f"Fejl ved afsendelse: {e}")

if __name__ == "__main__":
    while True:
        send_plate(PLATE)
        time.sleep(10)  # 10 sekunder før næste afsendelse
