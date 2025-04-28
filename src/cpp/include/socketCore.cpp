#include "socketCore.h"

Socket::Socket(const char* ip, int port)
{
    addr_server_.sin_addr.s_addr = inet_addr(ip);
    addr_server_.sin_port        = htons(port);
    addr_server_.sin_family      = AF_INET;
}

void Socket::Connect()
{
    socket_ = socket(AF_INET, SOCK_STREAM, 0);
    connect(socket_, (sockaddr *)&addr_server_, sizeof(addr_server_));
}

int Socket::SendText(const char* text)
{
    return send(socket_, text, std::strlen(text), 0);
}

int Socket::RecvText(char* text)
{
    ssize_t recv_size = recv(socket_, text, 1024, 0);
    text[recv_size] = '\0';
    return recv_size;
}