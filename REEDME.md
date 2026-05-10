## Diagram 1. General Architecture (Layered)

```mermaid
flowchart LR
    PL[ConsoleMenu]
    API[WebAPI]
    BLL[Business Logic Layer]
    DAL[Data Access Layer]
    DB[(InMemory Database)]
    Contracts["Contracts (DTOs)"]

    PL -->|HttpClient| API
    API --> BLL
    BLL --> DAL
    DAL --> DB

    PL --> Contracts
    API --> Contracts
    BLL --> Contracts
```

---
## Diagram 2. ConsoleMenu

```mermaid
classDiagram
    class MenuActionHandler {
        -RoomApiClient roomClient
        -BookingApiClient bookingClient
        -ActivityApiClient activityClient

        +ShowRooms()
        +ShowBookings()
        +CreateBooking()
        +UpdateBooking()
        +DeleteBooking()
        +ShowActivities()
        -ChooseActivities()
    }

    class RoomApiClient {
        +GetAllRoomsAsync()
    }

    class BookingApiClient {
        +GetAllBookingsAsync()
        +GetByIdAsync(id)
        +CreateBookingAsync(dto)
        +UpdateBookingAsync(id, dto)
        +DeleteBookingAsync(id)
    }

    class ActivityApiClient {
        +GetAllActivitiesAsync()
    }

    MenuActionHandler --> RoomApiClient
    MenuActionHandler --> BookingApiClient
    MenuActionHandler --> ActivityApiClient
```

---
## Diagram 3. WebAPI

```mermaid
classDiagram
    class BookingController {
        +Get()
        +GetById(id)
        +Create(dto)
        +Update(id, dto)
        +Delete(id)
    }

    class RoomController {
        +Get()
    }

    class ActivityController {
        +Get()
    }

    class IBookingService
    class IRoomService
    class IActivityService

    BookingController --> IBookingService
    RoomController --> IRoomService
    ActivityController --> IActivityService
```

---
## Diagram 4. Business Logic Layer

```mermaid
classDiagram
    class BookingService {
        -IUnitOfWork uow
        -IMapper mapper
        -DbContext context

        +CreateBookingAsync(dto)
        +UpdateBookingAsync(id, dto)
        +DeleteBookingAsync(id)
        +GetBookingsAsync()
        +GetByIdAsync(id)
        +IsRoomAvailable(roomId, start, end, excludeId)
    }

    class RoomService
    class ActivityService

    class IUnitOfWork {
        +Bookings
        +Activities
        +Rooms
        +SaveAsync()
    }

    BookingService --> IUnitOfWork
    RoomService --> IUnitOfWork
    ActivityService --> IUnitOfWork
```

---
## Diagram 5. Data Access Layer

```mermaid
classDiagram
    class AntiCafeDbContext {
        +DbSet~Booking~
        +DbSet~Room~
        +DbSet~Activity~
    }

    class Booking {
        +Id
        +RoomId
        +StartTime
        +EndTime
        +IsFullService
        +Activities
    }

    class Room {
        +Id
        +Name
        +Capacity
    }

    class Activity {
        +Id
        +Name
    }

    class Repository~T~ {
        +GetByIdAsync()
        +GetAllAsync()
        +FindAsync()
        +AddAsync()
        +Update()
        +Delete()
    }

    class UnitOfWork {
        +Bookings
        +Rooms
        +Activities
        +SaveAsync()
    }

    UnitOfWork --> Repository
    AntiCafeDbContext --> Booking
    AntiCafeDbContext --> Room
    AntiCafeDbContext --> Activity
```

---
## Diagram 6. Contracts (DTO)

```mermaid
classDiagram
    class BookingDto {
        +Id
        +RoomId
        +StartTime
        +EndTime
        +IsFullService
        +Activities
    }

    class ActivityDto {
        +Id
        +Name
    }

    class RoomDto {
        +Id
        +Name
        +Capacity
    }

    BookingDto --> ActivityDto
```
