FROM microsoft/dotnet-nightly:2.2-sdk-alpine as build
WORKDIR /src

COPY NuGet.config .
COPY ./DispatchR.Web/*.csproj ./DispatchR.Web/
COPY ./DispatchR.Data/*.csproj ./DispatchR.Data/

RUN dotnet restore ./DispatchR.Web/DispatchR.Web.csproj

COPY ./DispatchR.Web/ ./DispatchR.Web
COPY ./DispatchR.Data/ ./DispatchR.Data

RUN dotnet publish -c release ./DispatchR.Web/DispatchR.Web.csproj -o /out

FROM microsoft/dotnet-nightly:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /out/* ./
ENTRYPOINT [ "dotnet", "DispatchR.Web.dll" ]