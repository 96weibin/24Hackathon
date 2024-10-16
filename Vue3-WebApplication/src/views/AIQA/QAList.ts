import { Options, Vue } from "vue-class-component";
import { Search } from '@element-plus/icons-vue';
import { DateUtil } from "../../utils/dateUtil";
import { ElMessageBox } from "element-plus";
import questionList from './question-list.vue';
import { QuestionService } from '../../services/QuestionService';

@Options({
    props: {

    },
    components: {
        Search,
        questionList
    }
})

export default class QAList extends Vue {
    questionService = new QuestionService();
    typeValue: string = '';
    options = [];
    totalCount = 0;
    currentPage = 0;
    size = 10;
    defaultCheck = "0";
    showDown = true;
    order = 0;
    questionList: {
        title: string,
        name: string,
        content: string,
        time: string,
        replyCommentCount: number,
        likeNumber: number,
        state: string
    }[] = [];
    userId: number;
    created() {
        // this.userService.getLoginUser().then(res => {
        //     this.userId = res.data.id;
        // });
        // this.getData();
        this.userId = Number(localStorage.getItem('userId'));
        this.questionService.getAllQuestionTypes().then(res => {
            this.options = res.data.map(x => ({ label: x.type, value: x.id.toString(), id: x.id }));
        });
        this.questionService.getAllQuestionsCount().then(res => {
            this.totalCount = res.data;
        });
        this.getQuestions(this.currentPage);
    }



    mounted() {

    }

    Totext(html) {
        let input = html + "";
        return input.replace(/<(style|script|iframe)[^>]*?>[\s\S]+?<\/\1\s*>/gi, '').replace(/<[^>]+?>/g, '').replace(/\s+/g, ' ').replace(/ /g, ' ').replace(/</g, ' ').replace(/>/g, ' ');
    }


    filterQuestion() {
        this.getQuestions(this.currentPage);
    }

    filterType() {
        debugger;
    }
    handleCurrentChange(index) {
        this.getQuestions(index);
    }
    changeBtn() {
        this.getQuestions(this.currentPage);
    }

    getQuestions(currentPage: number) {
        let order = this.showDown ? 1 : 0;
        this.questionService.getQuestions(this.userId, currentPage, this.size, Number(this.defaultCheck), order).then(res => {
            this.questionList = res.data.map(x => ({
                title: x.title,
                name: x.personName,
                content: x.content,
                time: x.createDate,
                replyCommentCount: x.comments.filter(x => !x.isRefer).length,
                likeNumber: 0,
                state: x.state,
                id: x.id
            }));

        });
    }

    filterQuestionByProduct() {
        debugger;
        this.questionService.filterQuestionByProduct(Number(this.typeValue)).then(res => {
            this.questionList = res.data.map(x => ({
                title: x.title,
                name: x.personName,
                content: x.content,
                time: x.createDate,
                replyCommentCount: x.comments.filter(x => !x.isRefer).length,
                likeNumber: 0,
                state: x.state,
                id: x.id
            }));
        });
    }
}