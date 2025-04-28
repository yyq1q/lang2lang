#pragma once

#include <iostream>
#include <cstring>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

class Socket
{
protected:
    int socket_;
    struct sockaddr_in addr_server_;

public:
    Socket(const char* ip, int port);
    virtual void Connect();
    int SendText(const char* text);
    int RecvText(char* text);
};