.PHONY: frontend backend dev

frontend:
	cd frontend/customer-communication && yarn install && yarn dev

backend:
	cd backend/ChatApp.Api && dotnet run

dev:
	make -j2 frontend backend
