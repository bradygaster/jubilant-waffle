FROM microsoft/dotnet-nightly:2.2-sdk-alpine as build
WORKDIR /src

COPY NuGet.config .
COPY ./DispatchR.Api/*.csproj ./DispatchR.Api/
COPY ./DispatchR.Data/*.csproj ./DispatchR.Data/

RUN dotnet restore ./DispatchR.Api/DispatchR.Api.csproj

COPY ./DispatchR.Api/ ./DispatchR.Api
COPY ./DispatchR.Data/ ./DispatchR.Data

RUN dotnet publish -c release ./DispatchR.Api/DispatchR.Api.csproj -o /out

FROM microsoft/dotnet-nightly:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build /out/* ./
ENTRYPOINT [ "dotnet", "DispatchR.Api.dll" ]