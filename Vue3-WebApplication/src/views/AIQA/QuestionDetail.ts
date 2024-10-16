import { RoleType } from './../../services/QuestionService';
import { Options, Vue } from "vue-class-component";
import { IAddCommentRequest, ICommentContract, IUpdateCommentRequest, QuestionService } from '../../services/QuestionService';
import basicInfo from './basic-info.vue'
import replyCard from './reply-card.vue'
import { useRoute, useRouter } from 'vue-router';
import { DateUtil } from "../../utils/dateUtil";
import { useStore } from "vuex";

@Options({
    props: {

    },
    components: {
        basicInfo,
        replyCard
    }
})
export default class QuestionDetail extends Vue {
    questionService = new QuestionService();
    questionTitle: string = '';
    content: string = '';
    name: string = '';
    createTime: string = '';
    route = useRoute();
    store = useStore();
    questionId = Number(this.route.params.id);
    router = useRouter();
    myComment: string = '';
    showEdit: boolean = false;
    replyCount: number = 0;
    referCount: number = 0;
    replyName: string = '';
    replyDate: string = '';
    replyContent: string = '';
    likeCount: number = 0;
    replyComments: ICommentContract[];
    questionType: string = '';
    referComment: string = '';
    rndPeople: { value: string, label: string, id: number }[] = [];
    referComments: ICommentContract[] = [];
    value: { value: string, label: string, id: number }[] = [];
    showSummary = false;
    mySummary: string = '';
    familyName: string = '';
    disableSummaryBtn = true;
    showRefer = true;
    userId: number;
    hideSummary = true;
    created() {
        if (Number(localStorage.getItem('userRole')) == RoleType.Customer) {
            this.showRefer = false;
            this.hideSummary = false;
        }
        this.userId = Number(localStorage.getItem('userId'));
        this.questionService.getQuestionDetail(this.questionId).then(res => {
            this.questionTitle = res.data.title;
            this.content = res.data.content;
            this.name = res.data.personName;
            this.createTime = DateUtil.formatShortDate(res.data.createDate);
            this.questionType = res.data.questionType;
            this.replyComments = res.data.comments.filter(x => !x.isRefer);
            this.referComments = res.data.comments.filter(x => x.isRefer);
            this.referCount = this.referComments.length;
            this.replyCount = this.replyComments.length;
            this.questionType = res.data.questionType;
            this.familyName = res.data.familyName;
            this.mySummary = res.data.summary;
            this.disableSummaryBtn = res.data.state == "Close" ? true : false;
            this.value = res.data.referPeople.map(x => ({ id: x.id, label: x.name, value: x.id.toString() }));
        });
        this.questionService.getRNDPeople().then(res => {
            this.rndPeople = res.data.map(x => ({ id: x.id, label: x.name, value: x.id.toString() }))
            console.log(res);
        });
    }
    edit() {
        this.router.push({ path: '/ask/', query: { questionId: this.questionId } });
    }
    reply() {
        this.showEdit = true;
    }
    cancel() {
        this.showEdit = false;
    }
    submit() {
        this.addComment(false, this.myComment);
    }

    selectPeople() {
        console.log(this.value);
        let peopleIds = this.value.map(x => x.id);
        this.questionService.referPeople(this.questionId, peopleIds);
        debugger;
    }

    referCommentSubmit() {
        this.addComment(true, this.referComment);
    }

    addComment(isRefer: boolean, content) {
        let comment: IAddCommentRequest = {
            userId: this.userId,
            questionId: this.questionId,
            content: content,
            parentCommentId: null,
            isRefer: isRefer
        }
        this.questionService.addComment(comment).then(res => {
            debugger;
            if (res.data.isRefer) {
                this.referComments.push(res.data);
                this.referCount++;
            } else {
                this.replyCount++;
                this.showEdit = false;
                this.replyComments.push(res.data);
            }
        });
    }

    referCommentCancel() {
        this.myComment = "";
    }

    closeWithSummary() {
        this.showSummary = true;
    }
    closeNoSummary() {
        this.questionService.updateQuestionState(this.questionId).then(res => {
            if (res) {
                this.disableSummaryBtn = true;
            }
        });
        debugger;
    }
    cancelSummary() {
        this.showSummary = false;
    }
    submitSummary() {
        this.questionService.addSummaryToQuestion(this.mySummary, this.questionId).then(res => {
            this.showSummary = false;
            this.router.push('/qaList');
        });

    }
}