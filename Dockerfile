FROM mcr.microsoft.com/dotnet/core/sdk:3.1

ENV SWIG_VERSION 4.0.2
ENV GO_VERSION 1.15.2

# Install gcc
RUN apt-get update && \
    apt-get install -y build-essential
    
# Install gcc-mingv-w64
RUN apt install -y gcc-mingw-w64

# Install PCRE
RUN apt-get install -y libpcre3-dev

#install swig
RUN wget https://downloads.sourceforge.net/swig/swig-$SWIG_VERSION.tar.gz && \
	mkdir swig && tar -xzvf swig-$SWIG_VERSION.tar.gz -C swig --strip-components 1

RUN cd swig && \
	./configure --prefix=/opt/swig --without-maximum-compile-warnings && \
    make && make install
RUN rm -rf swig swig-$SWIG_VERSION.tar.gz

ENV SWIG_DIR /opt/swig/share/swig/$SWIG_VERSION/
ENV SWIG_EXECUTABLE /opt/swig/bin/swig
ENV PATH $PATH:/opt/swig/bin/

# Install Go
RUN wget https://dl.google.com/go/go$GO_VERSION.linux-amd64.tar.gz && \
	tar -C /usr/local -xzf go$GO_VERSION.linux-amd64.tar.gz && \
	rm go$GO_VERSION.linux-amd64.tar.gz

ENV GOROOT /usr/local/go/
ENV PATH $PATH:$GOROOT/bin/

# Install SecretHub
RUN echo "deb [trusted=yes] https://apt.secrethub.io stable main" > /etc/apt/sources.list.d/secrethub.sources.list && \
	apt-get update && \
	apt-get install -y secrethub-cli

WORKDIR /home/secrethub-dotnet/
RUN adduser --disabled-login --gecos "" -q user-dotnet
USER user-dotnet
