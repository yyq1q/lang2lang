cmake_minimum_required(VERSION 3.10)

project(lang2lang LANGUAGES CXX)
set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED True)

set(SERVER_SOURCES
    src/server.cpp
    include/socketCore.cpp
    include/socketServer.cpp
)

set(CLIENT_SOURCES
    src/client.cpp
    include/socketCore.cpp
    include/socketClient.cpp
)

include_directories(
    ${CMAKE_SOURCE_DIR}/include
)

add_executable(server ${SERVER_SOURCES})

add_executable(client ${CLIENT_SOURCES})