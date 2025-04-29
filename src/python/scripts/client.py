import socket
import threading

host = '127.0.0.1'  # サーバーのIPアドレス
port = 2001         # サーバーのポート番号

client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client_socket.connect((host, port))
        
def recv():
    while True:
        msg = client_socket.recv(1024).decode()
        print(f"サーバーからの応答: {msg}")
        
def send():
    while True:
        message = input("送信メッセージ: ")
        if message.lower() == 'exit':
            print("接続を終了します。")
            break
        client_socket.send(message.encode())
        
task1 = threading.Thread(target=recv)
task2 = threading.Thread(target=send)

task1.start()
task2.start()