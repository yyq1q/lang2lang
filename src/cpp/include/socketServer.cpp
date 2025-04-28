#include "socketServer.h"

SocketServer::SocketServer(int port) : Socket("0.0.0.0", port)
{
    addr_server_.sin_addr.s_addr = INADDR_ANY;
    addr_server_.sin_port        = htons(port);
    addr_server_.sin_family      = AF_INET;
}

SocketServer::SocketServer(const char* ip, int port) : Socket(ip, port)
{
    addr_server_.sin_addr.s_addr = inet_addr(ip);
    addr_server_.sin_port        = htons(port);
    addr_server_.sin_family      = AF_INET;
}

void SocketServer::Connect()
{
    socket_ = socket(AF_INET, SOCK_STREAM, 0);
    bind(socket_, (sockaddr*)&addr_server_, sizeof(addr_server_));
    listen(socket_, SOMAXCONN);
    socklen_t addr_len = sizeof(addr_client_);
    socket_ = accept(socket_, (sockaddr *)&addr_client_, &addr_len);
}