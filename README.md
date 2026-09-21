 
 [문법 정리 ](#.proto-문법-완벽-정리-(Proto3-기준))
 ## protobuf 
 [``]
 Google Protocol Buffers(Protobuf)는 구글이 개발한 구조화된 데이터를 직렬화(Serialization)하는 직관적이고 효율적인 매커니즘입니다.

 쉽게 말해, 서로 다른 언어나 시스템 간에 데이터를 가장 작은 용량과 빠른 속도로 주고받기 위한 규격입니다.

---
# 1.Protobuf의 핵심 개념 및 동작 원리  
Protobuf는 기본적으로 "선 정의, 후 생성" 방식으로 동작합니다.
- .proto 파일 정의: IDL(Interface Definition Language)을 사용해 주고받을 데이터 구조(Schema)를 정의합니다.
- protoc 컴파일: 프로토콜 버퍼 컴파일러(protoc)를 사용해 원하는 언어(C#, C++, Java, Python, Go 등)의 소스 코드로 변환합니다.

- 직렬화 및 역직렬화: 생성된 클래스를 프로젝트에 가져와 객체를 바이너리(Binary) 데이터로 바꾸거나(직렬화), 바이너리를 객체로 복원(역직렬화)합니다.

---

# 2. JSON / XML과의 차이점 (왜 쓸까?)
| 특징 | JSON / XML | Protocol Buffers |
|---|---|---|
| 데이터 형태 | Human-readable 텍스트 | Compact 바이너리 (Binary) |
| 용량 및 속도 | 필드명까지 텍스트로 전달되어 용량이 큼 | 필드 번호(Tag) 기반 직렬화로 용량이 작고 빠름 |
| 스키마 (Schema) | 선택적 (Dynamic / Loose) | 필수 (`.proto` 파일) |
| 타입 안정성 | 주로 런타임에서 오류 확인 | 컴파일 시점에 타입 검증 |
| 주요 사용처 | Web REST API | 게임 서버 통신, MSA (Microservices), gRPC |

---
## 3. .proto 문법 완벽 정리 (Proto3 기준)
```
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
  int32 id = 1;              // 정수형
  string username = 2;       // 문자열
  UserRole role = 3;         // Enum 타입
  
  repeated string tags = 4;  // repeated = 리스트/배열 타입
  
  map<string, string> attributes = 5; // Key-Value 맵 타입
}

// 6. 메시지 중첩 (Nested Message)
message Group {
  int32 group_id = 1;
  repeated UserProfile members = 2; // 다른 메시지를 타입으로 사용
}
```
필드 번호(Tag Number) 주의사항
id = 1, username = 2에서 숫자 1, 2는 데이터의 값이 아니라 **바이너리상에서 해당 필드를 식별하는 고유 번호(Tag)**입니다.

1~15번: 1바이트로 인코딩되므로 자주 쓰이는 필드에 할당하는 것이 유리합니다.

한번 지정된 필드 번호는 절대 변경하면 안 됩니다. (하위 호환성 깨짐)