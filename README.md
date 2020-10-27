# containerchat
A simple chat website that runs in containers with asp.net core, redis and sql server.

# Prerequisites for Ubuntu Workstation
1) Install docker: https://docs.docker.com/engine/install/ubuntu/
2) Install docker-compose: https://docs.docker.com/engine/install/ubuntu/
3) Install vscode: https://code.visualstudio.com/docs/setup/linux
     * Note: Snap Install is not recommended
4) Install dotnet core 3.1: https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu
5) Clone source code from Git Repository:
     * mkdir ~/git
     * cd ~/git
     * git clone https://github.com/ThePumpkinKingHacker/containerchat.git

# Running Locally
1) Create local docker image for web project:

     * cd ~/git/containerchat/web
     * docker build -t web .

2) Run docker-compose:

     * cd ~/git/containerchat/setup
     * docker-compose up -d

3) Visit "http://localhost:8080" in web browser.  

     * Note you must wait 90 seconds for SQL to populate.

# Debugging Locally
1) Start the Redis and SQL locally with docker-compose.dev.yml file:

     * cd ~/git/containerchat/setup
     * docker-compose -f docker-compose.dev.yml up -d

2) Run app with dotnet

     * cd ~/git/containerchat/web
     * dotnet run

2a) Alternatively, use vscode to open ~/git/containerchat/web
