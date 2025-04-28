#include "socketClient.h"

SocketClient::SocketClient(const char* ip, int port) : Socket(ip, port)
{
    addr_server_.sin_addr.s_addr = inet_addr(ip);
    addr_server_.sin_port        = htons(port);
    addr_server_.sin_family      = AF_INET;
}