import socket

def start_server():
    host = '127.0.0.1'  # ローカルホスト
    port = 2001         # ポート番号

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((host, port))
    server_socket.listen(1)
    print(f"サーバーが {host}:{port} で待機中...")

    conn, addr = server_socket.accept()
    print(f"接続されました: {addr}")

    while True:
        data = conn.recv(1024).decode()
        if not data:
            break
        print(f"受信: {data}")
        conn.send(f"サーバーからの応答: {data}".encode())

    conn.close()

if __name__ == "__main__":
    start_server()