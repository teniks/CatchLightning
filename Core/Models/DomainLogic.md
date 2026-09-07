```mermaid
erDiagram
direction LR
Achievement }o--o{ Achievement : "Содержит шаги"
Achievement }o--|| Goal : "Содержит шаги"
Category ||--o{ Goal : "Содержит цели"
Achievement }o..|| Category : "Косвенно относятся"

Achievement {
    int ID PK
    strin Name
    string? Description
    int Level
    bool IsCompleted
    int Goal FK
    int Category FK
    int[] Conditions FK
    int[] Dependents FK
}

Goal {
    int ID PK
    string Name
}

Category {
    int ID PK
    string Name
    string? Description
}
```