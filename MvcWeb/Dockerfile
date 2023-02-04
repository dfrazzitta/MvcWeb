#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


RUN apt-get update
RUN apt-get update && \
    apt-get install  -y \
        apt-transport-https \
        arping \
        arptables \
        bridge-utils \
        ca-certificates \
        conntrack \
        curl \
        dnsutils \
        ethtool \
        gdb \
        iperf \
        iperf3 \
        iproute2 \
        nano \
        tar \
        ipset \
        iptables \
        iputils-ping \
        jq \
        kmod \
        ldap-utils \
        less \
        libpcap-dev \
        net-tools \
        netcat \
        netcat-openbsd \
        openssl \
        procps \
        nfs-kernel-server \
        psmisc \
       # python-dbg \
       # python3-dbg \
        socat \
        software-properties-common \
        strace \
        tcpdump \
        telnet \
        tmux \
        tree \
        vim \
        wget && \
    rm -rf /var/lib/apt/lists/*

COPY ./Simple.pfx /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MvcWeb/MvcWeb.csproj", "MvcWeb/"]
RUN dotnet restore "MvcWeb/MvcWeb.csproj"
COPY . .
WORKDIR "/src/MvcWeb"
RUN dotnet build "MvcWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MvcWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcWeb.dll"]