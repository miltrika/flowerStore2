FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /app
EXPOSE 80

COPY *.props .
COPY *.sln .

COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY Contracts.DAL.Base/*.csproj ./Contracts.DAL.Base/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY DAL.Base/*.csproj ./DAL.Base/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/


COPY Contracts.Domain.Base/*.csproj ./Contracts.Domain.Base/
COPY Domain.App/*.csproj ./Domain.App/
COPY Domain.Base/*.csproj ./Domain.Base/

COPY PublicApi.DTO.v1/*.csproj ./PublicApi.DTO.v1/

COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore

COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY Contracts.DAL.Base/. ./Contracts.DAL.Base/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY DAL.Base/. ./DAL.Base/
COPY DAL.Base.EF/. ./DAL.Base.EF/

COPY Contracts.Domain.Base/. ./Contracts.Domain.Base/
COPY Domain.App/. ./Domain.App/
COPY Domain.Base/. ./Domain.Base/

COPY PublicApi.DTO.v1/. ./PublicApi.DTO.v1/

COPY WebApp/. ./WebApp/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:PsqlConnection="Host=postgres;Port=5432;Database=pvt;User Id=flowerUser;Password=flowerStore"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]

