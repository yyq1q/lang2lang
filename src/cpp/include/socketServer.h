#pragma once

#include "socketCore.h"

class SocketServer : public Socket
{
private:
    struct sockaddr_in addr_client_;

public:
    SocketServer(int port);
    SocketServer(const char* ip, int port);
    void Connect() override;
};