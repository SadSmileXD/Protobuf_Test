
## 목차

- [ Protobuf의 핵심 개념 및 동작 원리](#protobuf의-핵심-개념-및-동작-원리)
- [ JSON / XML과의 차이점](#json--xml과의-차이점-왜-쓸까)
- [ .proto 문법 완벽 정리](#proto-문법-완벽-정리-proto3-기준)
- [필드 번호(Tag Number) 주의사항](#필드-번호tag-number-주의사항)
- [설치 방법](#설치방법)
---

# Protobuf의 핵심 개념 및 동작 원리

Protobuf는 기본적으로 "선 정의, 후 생성" 방식으로 동작합니다.

- `.proto` 파일 정의: IDL(Interface Definition Language)을 사용해 주고받을 데이터 구조(Schema)를 정의합니다.
- `protoc` 컴파일: 프로토콜 버퍼 컴파일러(protoc)를 사용해 원하는 언어(C#, C++, Java, Python, Go 등)의 소스 코드로 변환합니다.
- 직렬화 및 역직렬화: 생성된 클래스를 프로젝트에 가져와 객체를 바이너리(Binary) 데이터로 바꾸거나(직렬화), 바이너리를 객체로 복원(역직렬화)합니다.

---

# JSON / XML과의 차이점 (왜 쓸까?)

| 특징 | JSON / XML | Protocol Buffers |
|---|---|---|
| 데이터 형태 | Human-readable 텍스트 | Compact 바이너리 (Binary) |
| 용량 및 속도 | 필드명까지 텍스트로 전달되어 용량이 큼 | 필드 번호(Tag) 기반 직렬화로 용량이 작고 빠름 |
| 스키마 (Schema) | 선택적 (Dynamic / Loose) | 필수 (`.proto` 파일) |
| 타입 안정성 | 주로 런타임에서 오류 확인 | 컴파일 시점에 타입 검증 |
| 주요 사용처 | Web REST API | 게임 서버 통신, MSA (Microservices), gRPC |

---

# .proto 문법 완벽 정리 (Proto3 기준)

```proto
// 1. 프로토버전 선언 (필수)

syntax = "proto3";

// 2. 패키지 선언 (이름 공간 충돌 방지)

package Game;

// 3. 타겟 언어별 옵션 (예: C# 네임스페이스 지정)

option csharp_namespace = "MyGame.Network";

// 4. 열거형 (Enum) - 첫 번째 값은 반드시 0이어야 함

enum UserRole {
    ROLE_GUEST = 0;
    ROLE_USER = 1;
    ROLE_ADMIN = 2;
}

// 5. 메시지 (Data Structure)

message UserProfile {
    // [타입] [필드명] = [필드 번호];

    int32 id = 1;
    string username = 2;
    UserRole role = 3;
    repeated string tags = 4;
    map<string, string> attributes = 5;
}

// 6. 메시지 중첩 (Nested Message)

message Group {
    int32 group_id = 1;
    repeated UserProfile members = 2;
}
```
## 필드 번호(Tag Number) 주의사항

id = 1, username = 2에서 숫자 1, 2는 데이터의 값이 아니라 **바이너리상에서 해당 필드를 식별하는 고유 번호(Tag)**입니다.

1~15번: 1바이트로 인코딩되므로 자주 사용하는 필드에 할당하는 것이 유리합니다.
한 번 지정된 필드 번호는 변경하거나 재사용하면 안 됩니다. (하위 호환성 문제)

---
# 설치방법
- Nugetforunity 설치  
![대체 텍스트](https://github.com/SadSmileXD/Protobuf_Test/blob/Google-Protobuf/image/1.png)  
![alt text](image.png)  
PackageManager에서 Add Package from git URL..클릭  
클릭 후 아래 링크 로 Nuget 설치
```
https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity
```

![alt text](image-1.png)
Nuget 클릭 -> Manage Nuget Packages 클릭    
![alt text](https://github.com/SadSmileXD/Protobuf_Test/blob/Google-Protobuf/image/4.png)  
![alt text](https://github.com/SadSmileXD/Protobuf_Test/blob/Google-Protobuf/image/3.png)  
Google.protobuf  와 Google.protobuf.Tools 설치

이러면 설치가 완료 되었다.
