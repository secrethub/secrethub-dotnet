from mcr.microsoft.com/dotnet/core/sdk:3.1

ENV SWIG_VERSION 4.0.2
ENV GO_VERSION 1.15.2

# Install gcc
RUN apt-get update && \
    printf "y" | apt-get install build-essential
    
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

# Prepare testing
RUN mkdir secrethub-dotnet/
WORKDIR /secrethub-dotnet/
COPY *.cs *.i *.csproj Makefile ./
COPY secrethub-xgo ./secrethub-xgo
COPY test ./test
RUN ls

CMD make dotnet-test
