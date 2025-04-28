#pragma once

#include "socketCore.h"

class SocketClient : public Socket
{
public:
    SocketClient(const char* ip, int port);
};