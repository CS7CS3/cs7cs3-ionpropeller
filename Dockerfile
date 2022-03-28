FROM mcr.microsoft.com/dotnet/sdk:6.0

ADD . /build

WORKDIR /build

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0

COPY --from=0 /out /app

CMD ["dotnet", "/app/IonPropeller.dll"]
