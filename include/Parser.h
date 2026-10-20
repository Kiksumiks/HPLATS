
typedef struct {
    char date[11];
    char time[9];
    char level[8];
    char thread_id[16];
    char message[256];
} LogEntry;

LogEntry* load_log_file(const char* filepath, int* out_count);
int parse_log_line(const char* line, LogEntry* out_entry);
void free_log_entrys(LogEntry* entrys);